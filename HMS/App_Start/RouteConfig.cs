using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            //Test
            routes.MapRoute(
name: "tests",
url: "tests",
defaults: new { controller = "Test", action = "TestList" }
);
            routes.MapRoute(
name: "add-test",
url: "add-test",
defaults: new { controller = "Test", action = "AddTest" }
);
            routes.MapRoute(
name: "delete-test",
url: "delete-test",
defaults: new { controller = "Test", action = "DeleteTest" }
);
            routes.MapRoute(
name: "delete-testCat",
url: "delete-test-category",
defaults: new { controller = "Test", action = "Delete" }
);
            routes.MapRoute(
name: "add-testCat",
url: "add-test-category",
defaults: new { controller = "Test", action = "AddTestCat" }
);
            routes.MapRoute(
name: "test-categories",
url: "test-categories",
defaults: new { controller = "Test", action = "TestCategory" }
);
            routes.MapRoute(
name: "add-attribute",
url: "add-attribute",
defaults: new { controller = "Test", action = "AddTestAttribute" }
);
            routes.MapRoute(
name: "test-attributes",
url: "test-attributes",
defaults: new { controller = "Test", action = "TestAttributes" }
);
            routes.MapRoute(
name: "delete-attribute",
url: "delete-attribute",
defaults: new { controller = "Test", action = "deleteattribute" }
);
           
            //End test
            routes.MapRoute(
name: "medicine",
url: "medicine",
defaults: new { controller = "Medicine", action = "MedicineList" }
);
            routes.MapRoute(
name: "add-medicine",
url: "add-medicine",
defaults: new { controller = "Medicine", action = "AddMedicine" }
);
            routes.MapRoute(
name: "delete-medicine",
url: "delete-medicine",
defaults: new { controller = "Medicine", action = "DeleteMedicine" }
);
            routes.MapRoute(
name: "medicine-categories",
url: "medicine-categories",
defaults: new { controller = "Medicine", action = "MedicineCategory" }
);
            routes.MapRoute(
name: "AddMedicineCat",
url: "add-medicine-category",
defaults: new { controller = "Medicine", action = "AddMedicineCat" }
);
            routes.MapRoute(
name: "Delete",
url: "delete-category",
defaults: new { controller = "Medicine", action = "Delete" }
);

            routes.MapRoute(
name: "PatientList",
url: "patients",
defaults: new { controller = "Patient", action = "PatientList" }
);

            routes.MapRoute(
name: "Register",
url: "register",
defaults: new { controller = "Login", action = "Register", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Login",
url: "Login",
defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
);

            routes.MapRoute(
name: "logout",
url: "logout",
defaults: new { controller = "Login", action = "logout", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "deletedoctor",
url: "delete-doctor",
defaults: new { controller = "Doctor", action = "Delete", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "doctorList",
url: "doctors",
defaults: new { controller = "Doctor", action = "DoctorList", id = UrlParameter.Optional }
);

            routes.MapRoute(
name: "getDesignationByQulif_id",
url: "getDesignationByQulif_id",
defaults: new { controller = "Doctor", action = "getDesignationByQulif_id", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "addDoctor",
url: "add-doctor",
defaults: new { controller = "Doctor", action = "addDoctor", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "PatientAppointment",
url: "patient-appointment",
defaults: new { controller = "Patient", action = "PatientAppointment", id = UrlParameter.Optional }
);
            routes.MapRoute(
   name: "patient-admission",
   url: "patient-admission",
   defaults: new { controller = "Patient", action = "PatientAdmission", id = UrlParameter.Optional }
);
   
            routes.MapRoute(
         name: "Patient",
         url: "add-patient",
         defaults: new { controller = "Patient", action = "AddPatient", id = UrlParameter.Optional }
     );
            
                            routes.MapRoute(
   name: "DeleteHospital",
   url: "delete-hospital",
   defaults: new { controller = "Hospital", action = "Delete" }
);
            routes.MapRoute(
   name: "HospitalList",
   url: "hospitals",
   defaults: new { controller = "Hospital", action = "ListOfHospitals" }
);
            routes.MapRoute(
         name: "Hospital",
         url: "add-hospital",
         defaults: new { controller = "Hospital", action = "AddHospital", id = UrlParameter.Optional }
     );
            
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
