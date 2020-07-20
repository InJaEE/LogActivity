using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using ITMSG.LogActivities.Activities.Design.Designers;
using ITMSG.LogActivities.Activities.Design.Properties;

namespace ITMSG.LogActivities.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            builder.AddCustomAttributes(typeof(ITMSGLog), categoryAttribute);
            builder.AddCustomAttributes(typeof(ITMSGLog), new DesignerAttribute(typeof(ITMSGLogDesigner)));
            builder.AddCustomAttributes(typeof(ITMSGLog), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(ITMSGLogWithPath), categoryAttribute);
            builder.AddCustomAttributes(typeof(ITMSGLogWithPath), new DesignerAttribute(typeof(ITMSGLogWithPathDesigner)));
            builder.AddCustomAttributes(typeof(ITMSGLogWithPath), new HelpKeywordAttribute(""));


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
