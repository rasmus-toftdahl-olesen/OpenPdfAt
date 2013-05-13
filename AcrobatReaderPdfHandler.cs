using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace OpenPdfAt
{
   public class AcrobatReaderPdfHandler : IPdfHandler
   {
      private string m_executable;

      public AcrobatReaderPdfHandler(string _executable)
      {
         m_executable = _executable;
      }

      public int OpenAtPage(int _page, string _filename)
      {
         Process.Start(m_executable, String.Format("/A \"page={0}\"  \"{1}\"", _page, _filename));
         return 0;
      }

      public int OpenAtNamedDest(string _nameddest, string _filename)
      {
         return 0;
      }
   }
}
