using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glamz.Business.Entity
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            _id = UniqueIdentifier.New;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id
        {
            get { return _id; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _id = UniqueIdentifier.New;
                else
                    _id = value;
            }
        }

        private string _id;


        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? EditedOn { get; set; }
        public int? EditedBy { get; set; }
    }

    public static class UniqueIdentifier
    {
        public static string New => ObjectId.GenerateNewId().ToString();
    }
}
