namespace CreateWorkPackages3.ProductBacklogItems.Model
{
    class ProductBacklogItemModel
    {
        public string Id { get; set; }
        public string WorkItemType { get; set; }
        public string Title { get; set; }
        public string AssignedTo { get; set; }
        public string Sprint { get; set; }
        public string Effort { get; set; }
        public string AreaPath { get; set; }
        public string RelatedFeatureTitle { get; set; }
        public string Url { get; set; }
    }
}
