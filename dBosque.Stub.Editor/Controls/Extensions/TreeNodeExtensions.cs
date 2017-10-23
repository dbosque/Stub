using dBosque.Stub.Editor.Controls.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using DB = dBosque.Stub.Repository.StubDb.Entities;

namespace dBosque.Stub.Editor.Controls.Extensions
{
    /// <summary>
    /// Treenode extensions
    /// </summary>
    public static class TreeNodeExtensions
    {
        public static NodeTag GetSelectedNodeTag(this TreeNode node, params NodeType[] types)
        {
            // Een node geselecteerd?
            if (node == null)
                return null;
            // En moet een valide NodeTag zijn.
            if (!(node.Tag is NodeTag))
                return null;
            var tag = node.Tag as NodeTag;
            return types.Length > 0 ? types.Contains(tag.nodeType) ? tag : null : tag;
        }

        public static IEnumerable<TreeNode> Flattened(this TreeNodeCollection collection)
        {
            foreach (TreeNode node in collection)
            {
                yield return node;
                foreach (TreeNode sub in node.Nodes.Flattened())
                    yield return sub;
            }
        }

        public static IEnumerable<TreeNode> ToTreeNode(this List<DB.Template> templates)
        {
            foreach (var template in templates)
                yield return template.ToTreeNode();
        }

        private static TreeNode ToTreeNode(this DB.Template template)
        {
            var treeNode = new TreeNode(template.Description)
            {
                Tag = new NodeTag()
                {
                    Key = template.TemplateId,
                    nodeType = NodeType.TemplateDescription
                },
                ToolTipText = "The name of a specific template."
            };
            template.Combination.ToList().ForEach(c =>
            {
                treeNode.Nodes.Add(c.ToTreeNode());
            }

            ) ;
            return treeNode;
        }

        private static TreeNode ToTreeNode(this DB.Combination combination)
        {
            var treeNode = new TreeNode(combination.Description)
            {
                Tag = new NodeTag()
                    {
                        Key = combination.ResponseId,
                        nodeType = NodeType.Response
                    },
                ToolTipText = "The name of a specific instance of a template."
            };
            var descriptions = new List<string>();
            foreach (var t in combination.CombinationXpath)
            {
                treeNode.Nodes.Add(t.ToTreeNode());
                descriptions.Add(t.XpathValue);
            }

            string defDescription = string.IsNullOrEmpty(combination.Description) ? string.Empty : combination.Description.Trim();
            if (string.IsNullOrEmpty(defDescription))
                treeNode.Text = "Combo : " + string.Join(",", descriptions.ToArray());
            return treeNode;
        }

        private static TreeNode ToTreeNode(this DB.CombinationXpath xpath)
        {
            var treeNode = new TreeNode(xpath.Xpath.CleanExpression)
            {
                ToolTipText = xpath.Xpath.CleanExpression,                
            };
          
            treeNode.Nodes.Add(new TreeNode(xpath.XpathValue)
            {
                Tag = new NodeTag()
                {
                    Key = xpath.CombinationXpathId,
                    nodeType = NodeType.XpathValue
                },
                ToolTipText = "This can be a specific value or a regular expression."
            });
            return treeNode;
        }

        /// <summary>
        /// Add an XMLNode to the given TreeNode
        /// </summary>
        /// <param name = "inTreeNode"></param>
        /// <param name = "inXmlNode"></param>
        public static void AddNodesToTree(this XmlNode inXmlNode, TreeNode inTreeNode)
        {
            foreach (XmlNode xNode in inXmlNode.ChildNodes)
            {
                if (xNode.NodeType == XmlNodeType.Element || xNode.NodeType == XmlNodeType.Text)
                {
                    TreeNode tNode = new TreeNode(xNode.Name)
                    {
                        Tag = xNode
                    };
                    inTreeNode.Nodes.Add(tNode);
                    if (xNode.HasChildNodes)
                    {
                        if (xNode.ChildNodes.Count > 1 || xNode.ChildNodes[0].NodeType != XmlNodeType.Text)
                            xNode.AddNodesToTree(tNode);
                        else
                            tNode.Text = $"<{xNode.Name}>{xNode.InnerText}</{xNode.Name}>";
                    }

                    foreach (XmlAttribute a in xNode.Attributes)
                    {
                        tNode.Nodes.Add(new TreeNode($"@{a.Name}:{a.Value}")
                        {
                            Tag = a
                        });
                    }
                }
            }
        }
    }
}