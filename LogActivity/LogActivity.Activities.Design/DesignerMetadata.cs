using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using LogActivity.Activities.Design.Designers;
using LogActivity.Activities.Design.Properties;

namespace LogActivity.Activities.Design
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


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
