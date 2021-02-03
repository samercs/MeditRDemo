using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UpdatePortal.Service;

namespace MeditRTest.Web.Core.Employee.CreateNotification
{
    public enum ActionType
    {
        Create,
        Edit,
        Delete
    }

    public class EmployeeNotification: INotification
    {
        public Data.Employee Employee { get; set; }
        public ActionType ActionType { get; set; }
    }

    public class CreateNotificationHandler: INotificationHandler<EmployeeNotification>
    {
        private readonly EmailService _emailService;
        public CreateNotificationHandler(EmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Handle(EmployeeNotification notification, CancellationToken cancellationToken)
        {

            switch (notification.ActionType)
            {
                case ActionType.Create:
                    await _emailService.SendEmail("samer_mail_2006@yahoo.com",
                        $"<div>Name: <b>{notification.Employee.Name}</b></div>" +
                        $"<div>Salary: <b>{notification.Employee.Salary}</b></div>" +
                        $"<div>Id: <b>{notification.Employee.Id}</b></div>", "Employee Created");
                    break;
                case ActionType.Delete:
                    await _emailService.SendEmail("samer_mail_2006@yahoo.com",
                        $"<div>Name: <b>{notification.Employee.Name}</b></div>" +
                        $"<div>Salary: <b>{notification.Employee.Salary}</b></div>" +
                        $"<div>Id: <b>{notification.Employee.Id}</b></div>", "Employee Delete");
                    break;
                case ActionType.Edit:
                    await _emailService.SendEmail("samer_mail_2006@yahoo.com",
                        $"<div>Name: <b>{notification.Employee.Name}</b></div>" +
                        $"<div>Salary: <b>{notification.Employee.Salary}</b></div>" +
                        $"<div>Id: <b>{notification.Employee.Id}</b></div>", "Employee Updated");
                    break;
            }
            

        }
    }
}