using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D2RModding_SpriteEdit.Forms
{
    class SpriteDirectoryTreeNode : TreeNode
    {
        public DirectoryInfo DirectoryInfo
        {
            get;
            private set;
        }

        public SpriteDirectoryTreeNode(DirectoryInfo dirInfo) : base(dirInfo.Name)
        {
            DirectoryInfo = dirInfo;
        }
    }
}
