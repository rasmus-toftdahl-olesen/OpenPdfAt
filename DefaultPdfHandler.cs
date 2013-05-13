using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace OpenPdfAt
{
   public class DefaultPdfHandler : IPdfHandler
   {
      public int OpenAtPage(int _page, string _filename)
      {
         Process.Start(_filename);
         return 0;
      }

      public int OpenAtNamedDest(string _nameddest, string _filename)
      {
         Process.Start(_filename);
         return 0;
      }
   }
}
