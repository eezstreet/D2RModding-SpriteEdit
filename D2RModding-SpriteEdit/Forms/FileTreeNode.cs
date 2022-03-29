using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D2RModding_SpriteEdit.Forms
{
    class FileTreeNode : TreeNode
    {
        public FileInfo FileInfo
        {
            get;
            private set;
        }

        public FileTreeNode(FileInfo fileInfo) : base(fileInfo.Name)
        {
            FileInfo = fileInfo;
        }
    }
}
