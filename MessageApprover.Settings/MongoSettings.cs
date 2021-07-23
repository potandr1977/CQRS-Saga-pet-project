namespace MessageApprover.Settings
{
    public static class MongoSettings
    {
        public const string ConnectionString = "mongodb://messageapprover_mongodb:27017/madb/";
        public const string DbName = "madb";
        public const string AuthorsCollectionName = "Authors";
        public const string EnteredMessagesCollectionName = "EnteredMessages";
    }
}

