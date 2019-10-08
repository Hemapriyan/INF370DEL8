using Snow_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Snow_System.Controllers
{
    public class TreeViewController : Controller
    {
        // GET: TreeView
        public ActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();
            SpartanFireDBEntities1 entities = new SpartanFireDBEntities1();

            //Loop and add the Parent Nodes.
            //foreach (Vehicle type in entities.Vehicles)
            //{
            //    nodes.Add(new TreeViewNode { id = type.VehicleTypeID.ToString(), parent = "#", text = type.Description });
            //}

            ////Loop and add the Child Nodes.
            //foreach (VehicleType subType in entities.VehicleTypes)
            //{
            //    nodes.Add(new TreeViewNode { id = subType.VehicleTypeID.ToString() + "-" + subType.VehicleTypeID.ToString(), parent = subType.VehicleTypeID.ToString(), text = subType.Description });
            //}


            //Loop and add the Parent Nodes.
            foreach (VehicleType type in entities.VehicleTypes)
            {
                nodes.Add(new TreeViewNode { id = type.VehicleTypeID.ToString(), parent = "#", text = type.Description });
            }

            //Loop and add the Child Nodes.
            foreach (Vehicle subType in entities.Vehicles)
            {
                nodes.Add(new TreeViewNode { id = subType.VehicleTypeID.ToString() + "-" + subType.VehicleTypeID.ToString(), parent = subType.VehicleTypeID.ToString(), text = subType.Description });
            }

            //Serialize to JSON string.
            ViewBag.Json = (new JavaScriptSerializer()).Serialize(nodes);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string selectedItems)
        {
            List<TreeViewNode> items = (new JavaScriptSerializer()).Deserialize<List<TreeViewNode>>(selectedItems);
            return RedirectToAction("Index");
        }


    }
}