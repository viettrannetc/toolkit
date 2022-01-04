using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BusinessLibrary.Ultilities
{
	public static class Constains
	{
		public static string Application = "View Clients";
		public static string TeamFunctionalTeamLead = "Soren";
		public static string TeamFunctional = "Functional Team";
		public static string TeamFunctionalA = "Functional Team A";
		public static string TeamFunctionalB = "Functional Team B";

		public static string TeamDesignTeamLead = "Anders Mikkelsen";
		public static string TeamDesign = "Design Team";
		public static string TeamSSTeamLead = "Mikkel";
		public static string TeamSS = "Shared service Team";
		public static string TeamSS1 = "SS - Authorization sub-team";
		public static string TeamSS2 = "SS - E2E Security and performance sub-team";
		public static string TeamSS3 = "SS- DevOps sub-team";
		public static string TeamSS4 = "SS- Functional back-end sub-team";
		public static string TeamQA = "QA";
		public static Color TeamQA_Color = ColorTranslator.FromHtml("#93c9a2"); //https://www.google.com/search?q=color+picker&rlz=1C1GCEA_enVN944VN944&oq=color+picker&aqs=chrome..69i57j0i67j0i512l8.2072j0j7&sourceid=chrome&ie=UTF-8
		public static Color TeamSS_Color = ColorTranslator.FromHtml("#e6cc65");
		public static Color TeamFUN_Color = ColorTranslator.FromHtml("#b8aabf");
		public static Color FE_Risk_Color = ColorTranslator.FromHtml("#a10d46");
		public static Color FE_Good_Color = TeamQA_Color;
		public static Color FE_OnTrack_Color = ColorTranslator.FromHtml("#fcfcfc");

		public static List<string> Team_Functional = new List<string>() { TeamFunctional, TeamFunctionalA, TeamFunctionalB };
		public static List<string> Team_SS = new List<string>() { TeamSS, TeamSS1, TeamSS2, TeamSS3, TeamSS4 };
		public static List<string> Team_Functional_Development = new List<string>() { TeamSS, TeamSS1, TeamSS2, TeamSS3, TeamSS4, TeamFunctional, TeamFunctionalA, TeamFunctionalB };

		public static string Release_Build = "Release 1.0 Build";
		public static string Release_Test = "Release 1.0 Test";
		public static List<string> Release_1st = new List<string>() { Release_Build, Release_Test };


		public static string FE_Status_New_10 = "10 - New";
		public static string FE_Status_Received_11 = "11 - Received";
		public static string FE_Status_USCreate_12 = "12 - US under creation";
		public static string FE_Status_PO_Review_15 = "15 - US ready for PO review";
		public static string FE_Status_NotApprove_20 = "20 - Not approved";
		public static string FE_Status_Approved_21 = "21 - Approved";
		public static string FE_Status_EstimationDone_25 = "25 - Estimation done";
		public static string FE_Status_AddedMVP_28 = "28 - Approved by MVP board";
		public static string FE_Status_AssignedDesign_30 = "30 - Assigned for design";
		public static string FE_Status_DesignInProcess_31 = "31 - Design in progress";
		public static string FE_Status_DesignReview_32 = "32 - Design ready for approval";
		public static string FE_Status_DesignNotApprove_33 = "33 - Design not approved";
		public static string FE_Status_DesignApprove_34 = "34 - Design approved";
		public static string FE_Status_EstimationUpdated_35 = "35 - Estimation updated";
		public static string FE_Status_AssignedToDev_36 = "36 - Assigned for implementation";
		public static string FE_Status_DevInProgress_37 = "37 - Implementation in progress";
		public static string FE_Status_DevImplemented_38 = "38 - Implemented";
		public static string FE_Status_AssignedToTest_40 = "40 - Migrated to test";
		public static string FE_Status_TestOk_51 = "51 - Test not ok";
		public static string FE_Status_TestNotOk_52 = "52 - Test ok";
		//public static string US_Status_New = "60 - Solved";
		//public static string US_Status_New = "80 - Awaiting customer";
		//public static string US_Status_New = "81 - Awaiting 3rd party";
		//public static string US_Status_New = "82 - Postponed";
		//public static string US_Status_New = "90 - Closed";
		//public static string US_Status_New = "91 - Rejected";
		public static string FE_Status_Duplicated = "92 - Duplicated";
		public static string FE_Status_Cancelled = "93 - Cancelled";
		public static List<string> FE_Status_In_Phase1_Initial = new List<string>() { FE_Status_New_10, US_Status_TestOK };
		public static List<string> FE_Status_In_Phase1_Analysis = new List<string>() { FE_Status_Received_11, FE_Status_USCreate_12 };
		public static List<string> FE_Status_In_Phase1_PO_Review = new List<string>() { FE_Status_PO_Review_15, FE_Status_NotApprove_20, FE_Status_Approved_21 };
		//public static List<string> FE_Status_In_Phase1_Dev_Analysis = new List<string>() { FE_Status_EstimationDone_25 };
		public static List<string> FE_Status_In_Phase1_Finallize = new List<string>() { FE_Status_EstimationDone_25, FE_Status_AddedMVP_28 };

		public static List<string> FE_Status_In_Phase2_Design = new List<string>() { FE_Status_AssignedDesign_30, FE_Status_DesignInProcess_31 };
		public static List<string> FE_Status_In_Phase2_Review = new List<string>() { FE_Status_DesignReview_32, FE_Status_DesignNotApprove_33, FE_Status_DesignApprove_34 };

		public static List<string> FE_Status_In_Phase3_Implementation = new List<string>() { FE_Status_AssignedToDev_36, FE_Status_DevInProgress_37, FE_Status_DevImplemented_38, FE_Status_TestNotOk_52 };
		public static List<string> FE_Status_In_Phase3_Implementated = new List<string>() { FE_Status_DevImplemented_38 };

		public static List<string> FE_Status_In_Phase4_Testing = new List<string>() { FE_Status_AssignedToTest_40, FE_Status_TestOk_51 };


		public static string US_Status_New = "10 - New";
		public static string US_Status_AC_Described = "20 - AC described";
		public static string US_Status_US_Approved = "25 - User Story Approved";
		public static string US_Status_ReadyDesign = "30 - Ready for design";
		public static string US_Status_DesignInProgress = "31 - Design in progress";
		public static string US_Status_Designed = "35 - Designed";
		public static string US_Status_ReadyForBuild = "40 - Ready for build";
		public static string US_Status_BuildInProgress = "45 - Build in progress";
		public static string US_Status_Implemented = "50 - Implemented";
		public static string US_Status_ReadyForUAT = "60 - Ready for UAT";
		public static string US_Status_TestOK = "62 - Test not ok";
		public static string US_Status_TestNotOK = "65 - Test ok";
		public static string US_Status_Solved = "70 - Solved";
		public static string US_Status_Blocked = "80 - Blocked";
		public static string US_Status_Closed = "90 - Closed";
		public static string US_Status_Cancelled = "93 - Cancelled";
		public static List<string> US_Status_Done_For_DEV_AND_Non_Technical = new List<string>() { US_Status_Implemented, US_Status_ReadyForUAT, US_Status_TestOK, US_Status_TestNotOK, US_Status_Solved, US_Status_Closed };
		public static List<string> US_Status_Done_For_TEST_AND_Non_Technical = new List<string>() { US_Status_ReadyForUAT, US_Status_TestOK, US_Status_TestNotOK, US_Status_Solved };
		public static List<string> US_Status_In_Analysis = new List<string>() { US_Status_New, US_Status_AC_Described, US_Status_US_Approved };
		public static List<string> US_Status_In_Design = new List<string>() { US_Status_ReadyDesign, US_Status_DesignInProgress, US_Status_Designed };
		public static List<string> US_Status_In_Implementation = new List<string>() { US_Status_ReadyForBuild, US_Status_Implemented, US_Status_TestNotOK, US_Status_Blocked };
		public static List<string> US_Status_In_Testing = new List<string>() { US_Status_ReadyForUAT, US_Status_TestOK };
		public static List<string> US_Status_In_Closed = new List<string>() { US_Status_Closed };


		public static string WP_Status_New = "10 - New";
		public static string WP_Status_Running = "31 - Running";
		public static string WP_Status_ReadyForReview = "32 - Ready for review";
		public static string WP_Status_TestNotOk = "40 - Test not ok";
		public static string WP_Status_Blocked = "70 - Blocked";
		public static string WP_Status_Postponed = "82 - Postponed";
		public static string WP_Status_Closed = "90 - Closed";
		public static string WP_Status_Cancelled = "93 - Cancelled";
		//public static List<string> WP_Status_BuildingPhase_Done = new List<string>() { WP_Status_Closed, WP_Status_Cancelled };


		public static string WP_TYPE_Build = "Build";
		public static string WP_TYPE_Other = "Other";
		public static string WP_TYPE_Analysis = "Analysis";
		public static string WP_TYPE_Automatic = "Automatic testing";
		public static string WP_TYPE_Functional_Design = "Functional design/UX";
		public static string WP_TYPE_Technical_Design = "Technical design";
		public static string WP_TYPE_Dev_Test = "Internal test";
		public static string WP_TYPE_Documentation = "Documentation";
		public static string WP_TYPE_Demo = "Demo feedback";
		/// <summary>
		/// Review WP in SS team
		/// </summary>
		public static string WP_TYPE_Governance = "Governance";
		public static List<string> WP_TYPE_BuildingPhase = new List<string>() { WP_TYPE_Documentation, WP_TYPE_Dev_Test, WP_TYPE_Build, WP_TYPE_Other, WP_TYPE_Analysis, WP_TYPE_Functional_Design, WP_TYPE_Technical_Design, WP_TYPE_Governance, WP_TYPE_Demo };
		//public static string WP_TYPE_   Test
		//public static string WP_TYPE_   Test - create tc
		//public static string WP_TYPE_   Test - review tc
		//public static string WP_TYPE_   Test - execution tc
		//public static string WP_TYPE_   Bug fixing
		//public static string WP_TYPE_   Workshop/Interview
		//public static string WP_TYPE_   Deliverable

		public static List<string> Status_Un_Expected = new List<string> { FE_Status_Cancelled, FE_Status_Duplicated };
		public static List<string> WP_Status_Un_Expected_For_Planning = new List<string> { WP_Status_Closed, FE_Status_Cancelled, FE_Status_Duplicated };
		public static List<string> FE_Status_For_Planning = new List<string> { WP_Status_Closed, FE_Status_AssignedToTest_40, FE_Status_TestOk_51 };


		public static int WorkingHours_PerDay = 8;
		public static string DayOff_Sat = "Saturday";
		public static string DayOff_Sunday = "Sunday";
		public static List<string> DayOff_InWeek = new List<string> { DayOff_Sat, DayOff_Sunday };

		public static List<string> FE_Priority = new List<string>() {
			"Drag and drop",
			"Sending and forwarding Messages",
			"Roles and Rights",
			"Reply flow",
			"Other Message Operations",
			"MeMo actions (new version)",
			"Handling errors and warnings",
			"Logo or initials, from sender company",
			"Legal notifications from public senders",
			"Folders",
			"Notes",
			"Login",
			"User Profile",
			"Usability tests",
			"Receive message and notification",
			"Onboarding End user",
			"CVR support in the view client",
			"New design",
			//"Logging",
			"View client API logging",
			"NgDP only version",
			"Terms and conditions",
		};

		public static List<string> WP_Priority = new List<string>() {
			"4073",
			"2975",
			"2485",
			"2457"
		};

		public static string Team_QA_Minh = "Võ Ngọc Nguyễn Minh";
		public static string Team_QA_Bao = "Nguyễn Quốc Bảo";
		public static string Team_QA_Viet = "Hoàng Trọng Việt";

		public static List<string> Team_QA_Members = new List<string>() { Team_QA_Minh, Team_QA_Bao, Team_QA_Viet };



		//public static string Team_QA_Minh = "Võ Ngọc Nguyễn Minh";
		//public static string Team_QA_Bao = "Nguyễn Quốc Bảo";
		//public static string Team_QA_Viet = "Hoàng Trọng Việt";

		public static Dictionary<int, string> Toolkit_Assignee = new Dictionary<int, string>() {
			{0, "" },
			{387, "Lâm Ngọc Thịnh" },
			{226, "Đồng Tấn Phát" },
			{217, "Nguyễn Phạm Long Duy" },
			{219, "Võ Ngọc Nguyễn Minh" },
			{211, "Nguyễn Huy An Đình" },
			{388, "Võ Thị Cẩm Linh" },//
			{389, "Phạm Thanh Phong" },
			{236, "Lê Châu Quang Huy" },
			{365, "Anders Beckmann Stok" },
			{520, "Ákos Tóth" },
			{209, "Søren Aksel Helbo Bjergmark" },
			{213, "Trần Xuân Việt" },
			{186, "Mateusz Markowski" },
			{228, "Cổ Hoàng Lân" },
			{321, "Mikkel Kier" },
			{392, "Jens Dybendal Hammelsvang" },
			{225, "Nguyễn Thị Diễm Trang" },
			{229, "Hoàng Trọng Việt" },
			{218, "Sebastian Weichelt" },
			{123, "Armen Nazarian" },
			{315, "Fie Tiller Hansen" },
			{130, "Danni Derakhshan" },
			{18, "Christian Lebek Jakobsen" },
			{355, "Łukasz Siwiński" },
			{70, "Anders Mikkelsen" }
		};
	}
}
