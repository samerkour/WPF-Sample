using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SarveenTech.SmartCattle.BackendSample.Gateway.FilterModels
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusEnum
    {
        None,
        Acked,
        Snoozed
    }


    public class NotificationFilterModel
    {
        [Description("1: Cattle Activity, 2: Barn, 3: Cattle Position")]
        public ICollection<byte> FilterByNotificationTypeId { get; set; }
        //public byte? FilterByNotificationActionTypeId { get; set; }
        public byte? FilterByNotificationLevelId { get; set; }
        public string FilterByTitle { get; set; }
        public string FilterByMessage { get; set; }
        public long? FilterBySubjectId { get; set; }
        public byte? FilterByEscalationLevel { get; set; }
        public StatusEnum FilterByStatus { get; set; }
        //public bool? FilterByIsSnoozed { get; set; }
        //public bool? FilterByIsAcked { get; set; }
        public DateTime? FilterByCreationDateTime { get; set; }
        //public DateTime? FilterByUpdateDateTime { get; set; }

    }

    public class NotificationSortOrderModel
    {
        public bool OrderByNotificationTypeAsc { get; set; }
        public bool OrderByNotificationLevelAsc { get; set; }
        public bool OrderByTitleAsc { get; set; }
        public bool OrderByMessageAsc { get; set; }
        public bool OrderBySubjectIdAsc { get; set; }
        public bool OrderByEscalationLevelAsc { get; set; }
        public bool OrderByStatusAsc { get; set; }
        public bool OrderByCreationDateTimeAsc { get; set; }
    }

    public class NotificationSearchOptionsModel
    {
        public string SearchByNotificationTypeTitle { get; set; }
        //public string SearchByNotificationActionTypeTitle { get; set; }
        public string SearchByNotificationLevelTitle { get; set; }
        public string SearchByTitle { get; set; }
        public string SearchByMessage { get; set; }
        public string SearchBySubject { get; set; }
        public string SearchByEscalationLevel { get; set; }
        //public string SearchByStatus { get; set; }

        public string SearchByComment { get; set; }

        public string SearchByAll { get; set; }

    }
}
