using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2RModding_SpriteEdit.Forms
{
    class SpriteDirectoryTreeNodeFactory
    {
        // Key: DirectoryInfo.FullName
        private Dictionary<string, SpriteDirectoryTreeNode> cache;

        public SpriteDirectoryTreeNodeFactory()
        {
            cache = new Dictionary<string, SpriteDirectoryTreeNode>();
        }

        public SpriteDirectoryTreeNode getNode(DirectoryInfo dirInfo)
        {
            if (!cache.ContainsKey(dirInfo.FullName))
            {
                var dirTreeNode = new SpriteDirectoryTreeNode(dirInfo);
                dirTreeNode.Nodes.AddRange(dirInfo.GetFiles("*.sprite").Select((fileInfo) => new FileTreeNode(fileInfo)).ToArray());
            
                dirTreeNode.Nodes.AddRange(dirInfo.GetDirectories().Select((subDirInfo) => {
                    var node = getNode(subDirInfo);
                    if (node.Parent != null)
                    {
                        node.Parent.Nodes.Remove(node);
                    }
                    if (node.TreeView != null)
                    {
                        node.Remove();
                    }
                    return node;
                }).ToArray());

                cache[dirInfo.FullName] = dirTreeNode;
            }

            return cache[dirInfo.FullName];
        }
    }
}
