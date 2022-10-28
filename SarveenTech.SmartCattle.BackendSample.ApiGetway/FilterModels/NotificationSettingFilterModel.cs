namespace SarveenTech.SmartCattle.BackendSample.Gateway.FilterModels
{
    public class NotificationSettingFilterModel
    {
        public long? FilterByNotificationTypeId { get; set; }

        public string FilterByTitle { get; set; }

        public string FilterByMessage { get; set; }

        public string FilterByPrimaryRole { get; set; }

        public byte? FilterByWindowInterval { get; set; }

        public byte? FilterByPeriodInterval { get; set; }

        public byte? FilterBySnoozeInterval { get; set; }

        public bool? FilterByIsEnabled { get; set; }

    }



    public class NotificationSettingSortOrderModel
    {
        public bool OrderByNotificationTypeAsc { get; set; }

        public bool OrderByTitleAsc { get; set; }

        public bool OrderByMessageAsc { get; set; }

        public bool OrderByPrimaryRoleAsc { get; set; }

        public bool OrderByWindowIntervalAsc { get; set; }

        public bool OrderByPeriodIntervalAsc { get; set; }

        public bool OrderBySnoozeIntervalAsc { get; set; }

        public bool OrderByIsEnabledAsc { get; set; }

    }



    public class NotificationSettingSearchOptionsModel
    {
        public string SearchByNotificationType { get; set; }

        public string SearchByTitle { get; set; }

        public string SearchByMessage { get; set; }

        public string SearchByPrimaryRole { get; set; }

        public string SearchByAll { get; set; }

    }


}
