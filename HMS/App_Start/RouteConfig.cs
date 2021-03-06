﻿using System;
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
            //Menu
            routes.MapRoute(
name: "Menu",
url: "Menu",
defaults: new { controller = "Home", action = "Menu" }
);

            //Test
            routes.MapRoute(
name: "tests",
url: "tests",
defaults: new { controller = "Test", action = "TestList" }
);
            routes.MapRoute(
name: "GetTestDetails",
url: "GetTestDetails",
defaults: new { controller = "Patient", action = "GetTestDetails", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "PatientMedList",
url: "patient-medicine",
defaults: new { controller = "Patient", action = "PatientMedList", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "PatientTestList",
url: "patient-tests",
defaults: new { controller = "Patient", action = "PatientTestList", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "PatientappointmentList",
url: "patient-appointments",
defaults: new { controller = "Patient", action = "PatientappointmentList", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "PatientAdmissionList",
url: "patient-admissions",
defaults: new { controller = "Patient", action = "PatientAdmissionList", id = UrlParameter.Optional }
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
name: "add-patient-medicine",
url: "add-patient-medicine",
defaults: new { controller = "Patient", action = "AddPatientMedicine", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "add-patient-test",
url: "add-patient-test",
defaults: new { controller = "Patient", action = "AddPatientTest", id = UrlParameter.Optional }
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
name: "delete-patient",
url: "delete-patient",
defaults: new { controller = "Patient", action = "Delete", id = UrlParameter.Optional }
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
            routes.MapRoute(
             name: "Dashboard",
             url: "dashboard",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
              name: "GeneratePatientBill",
              url: "GeneratePatientBill",
              defaults: new { controller = "PatientsBill", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            name: "PatientCreateBills",
            url: "patient-create-bill",
            defaults: new { controller = "PatientsBill", action = "Create", id = UrlParameter.Optional }
        );

            routes.MapRoute(
        name: "BillListings",
        url: "Bill_Listing",
        defaults: new { controller = "PatientsBill", action = "BillListings", id = UrlParameter.Optional }
    );
            routes.MapRoute(
  name: "DeleteBill",
  url: "DeleteBill",
  defaults: new { controller = "PatientsBill", action = "Delete", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "EditBill",
url: "EditBill",
defaults: new { controller = "PatientsBill", action = "Edit", id = UrlParameter.Optional }
);

            routes.MapRoute(
name: "EditDetailBill",
url: "EditDetailBill",
defaults: new { controller = "PatientsBill", action = "EditPatient", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Search",
url: "Search",
defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Navbar",
url: "Navbar",
defaults: new { controller = "Shared", action = "NavbarPatientInfo", id = UrlParameter.Optional }
);

            routes.MapRoute(
name: "UserRole",
url: "UserRolesPage",
defaults: new { controller = "UserRoles", action = "Index", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "UserRoleCreate",
url: "UserRolesCreate",
defaults: new { controller = "UserRoles", action = "Create", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "UserRoleEdit",
url: "UserRoleEdit",
defaults: new { controller = "UserRoles", action = "Edit", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "UserRoleDelete",
url: "UserRoleDelete",
defaults: new { controller = "UserRoles", action = "Delete", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "UserPages",
url: "role-pages",
defaults: new { controller = "UserRoles", action = "UserPages", id = UrlParameter.Optional }
);
            //
            //            routes.MapRoute(
            //name: "PrintTo PDF",
            //url: "Patients Bill Details",
            //defaults: new { controller = "PatientsBill", action = "PrintPartialViewToPdf", id = UrlParameter.Optional }
            //);


            routes.MapRoute(
name: "SessionDelete",
url: "Session_Delete",
defaults: new { controller = "PatientsBill", action = "DeleteBillSession", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Print",
url: "Print_Page",
defaults: new { controller = "PatientsBill", action = "Print", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "HospitalRooms",
url: "Hospital_Rooms",
defaults: new { controller = "HospitalRooms", action = "Index", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "HospitalRoomsCreate",
url: "Hospital_Rooms_Create",
defaults: new { controller = "HospitalRooms", action = "Create", id = UrlParameter.Optional }
);

            routes.MapRoute(
name: "Hospital_RoomsEdit",
url: "Hospital_Rooms_Edit",
defaults: new { controller = "HospitalRooms", action = "Edit", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Hospital_RoomsDelete",
url: "Hospital_Rooms_Delete",
defaults: new { controller = "HospitalRooms", action = "Delete", id = UrlParameter.Optional }
);


            //USER
            routes.MapRoute(
name: "DeleteUser",
url: "user-delete",
defaults: new { controller = "User", action = "delete", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "Adduser",
url: "add-user",
defaults: new { controller = "User", action = "AddUser", id = UrlParameter.Optional }
);
            routes.MapRoute(
name: "user-status",
url: "user-status",
defaults: new { controller = "User", action = "userStatusChange", UserId = UrlParameter.Optional, status= UrlParameter.Optional }
);
            routes.MapRoute(
name: "userList",
url: "user-list",
defaults: new { controller = "User", action = "UserList", id = UrlParameter.Optional }
); 
            //
        }
    }
}
