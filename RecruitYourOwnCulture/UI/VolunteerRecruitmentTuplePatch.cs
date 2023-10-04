using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using System.Collections.Generic;
using System.Xml;


#nullable enable
namespace RecruitYourOwnCulture.UI
{
    [PrefabExtension("RecruitVolunteerTuple", "descendant::NavigatableListPanel[@Id='NavigatableList']")]
    internal class VolunteerRecruitmentTuplePatch : PrefabExtensionInsertPatch
    {
        private readonly List<XmlNode> nodes;

        public override InsertType Type => (InsertType)2;

        public VolunteerRecruitmentTuplePatch()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<RecruitVolunteerTuplePatch/>");
            this.nodes = new List<XmlNode>()
            {
                (XmlNode) xmlDocument
            };
        }

        [PrefabExtensionInsertPatch.PrefabExtensionXmlNodes]
        public IEnumerable<XmlNode> Nodes => (IEnumerable<XmlNode>)this.nodes;
    }
}
