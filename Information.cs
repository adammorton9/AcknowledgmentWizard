using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrmg_ackowledgements
{
    public class Information
    {
        private string saveDir;
        private string savePDFAs;
        private int top_margin;

        //CMRE save variables
        private string saveDirCMRE;
        private string savePDFAsCMRE;
        private int top_marginCMRE;

        public string SaveDir
        {
            get { return saveDir; }
            set { saveDir = value; }
        }

        public string SavePDFAs
        {
            get { return savePDFAs; }
            set { savePDFAs = value; }
        }

        public int TopMargin
        {
            get { return top_margin; }
            set { top_margin = value; }
        }

        public string SaveDirCMRE
        {
            get { return saveDirCMRE; }
            set { saveDirCMRE = value; }
        }

        public string SavePDFAsCMRE
        {
            get { return savePDFAsCMRE; }
            set { savePDFAsCMRE = value; }
        }

        public int TopMarginCMRE
        {
            get { return top_marginCMRE; }
            set { top_marginCMRE = value; }
        }
    }
}
