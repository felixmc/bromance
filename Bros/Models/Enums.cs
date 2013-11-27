using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Enums
{
    public enum Gender { Female, Male }
    public enum Ethnicity { Asian, Native_American, Hispanic, Middle_Eastern, Indian, White, Black, Pacific_Islander, Other }
    public enum Children { Has_Children, Has_No_Children }
    public enum Religion { Agnostic, Atheist, Christian, Catholic, Jew, Muslim, Hindu, Buddhist, Other }
    public enum Education { High_School, College, Masters, Doctorate }
    public enum Pets { No_Pets, Dog_Person, Cat_Person, Animal_Lover }
    public enum Athleticism { Not_Athletic, Casually_Athletic, Very_Athletic }
    public enum MarriageStatus { Single, Taken, Married, Divorced, Widowed }
    public enum SexualOrientation { Heterosexual, Homosexual, Bisexual, Asexual, Pansexual, Other }
    public enum Smokes { Often, Sometimes, When_Drinking, Not_Anymore, Never }
    public enum Drinks { Often, Socially, Rarely, Not_Anymore, Never }
    public enum Drugs { Often, Sometimes, Rarely, Medicinally, Not_Anymore, Never }
    public enum Job
    {
        Student,
        Artistic_0_Musical_0_Writer,
        Banking_0_Financial_0_Real_Estate,
        Clerical_0_Administrative,
        Computer_0_Hardware_0_Software,
        Construction_0_Craftsmanship,
        Education_0_Academia,
        Entertainment_0_Media,
        Executive_0_Management,
        Hospitality_0_Travel,
        Law_0_Legal_Services,
        Medicine_0_Health,
        Military,
        Political_0_Government,
        Sales_0_Marketing_0_Biz_Dev,
        Science_0_Tech_0_Engineering,
        Transportation,
        Retired,
        Unemployed,
        Rather_Not_Say,
        Other
    }

	public static class Utils
	{
		public static T[] GetEnumValues<T>()
		{
			T[] items = (T[])Enum.GetValues(typeof(T));

			return items;
		}
	}
}