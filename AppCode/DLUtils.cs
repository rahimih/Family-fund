using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcessClass;
using BussinessClass;

namespace  DLibraryUtils
{
   public  class DLUtils
    {
       


        public BussinessClass.BussinessClass.userchecking usercheckingobj;
        public BussinessClass.BussinessClass.EventsLog EventsLogobj;
        public BussinessClass.BussinessClass.temperory temperoryobj;
        public BussinessClass.BussinessClass.temperory2 temperory2obj;
        public BussinessClass.BussinessClass.backuping backupobj;
         


        public DLUtils()
        {
            usercheckingobj = new BussinessClass.BussinessClass.userchecking();
            EventsLogobj = new BussinessClass.BussinessClass.EventsLog();
            temperoryobj = new BussinessClass.BussinessClass.temperory();
            temperory2obj = new BussinessClass.BussinessClass.temperory2();
            backupobj = new BussinessClass.BussinessClass.backuping();

       

        }
    }
}
