using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
	public enum Gender { Female, Male }
	public enum Ethnicity { Asian, Native_American, Hispanic, Middle_Eastern, Indian, White, Black, Pacific_Islander, Other }
	public enum Children { Has_Children, Has_No_Children }
	public enum Religion { Agnostic, Atheist, Christian, Catholic, Jew, Muslim, Hindu, Buddhist, Other }
	public enum Education { High_School, College, Masters, Doctorate }
	public enum Pets { No_Pets, Dog_Person, Cat_Person, Animal_Lover }
	public enum Athleticism { Not_Athletic, Casually_Athletic, Very_Athletic }
	public enum MariageStatus { Single, Taken, Married, Divorced, Widowed }
	public enum SexualOrientation { Heterosexual, Homosexual, Bisexual, Asexual, Pansexual, Other }
	public enum Smokes { Often, Sometimes, When_Drinking, Not_Anymore, Never }
	public enum Drinks { Often, Socially, Rarely, Not_Anymore, Never }
	public enum Drugs { Often, Sometimes, Rarely, Not_Anymore, Never }
	public enum Job {
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

    [Table("Profile")]
    public class Profile : Entity
    {
        [InverseProperty("Id")]
        [ForeignKey("User")]
		public virtual User Owner { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; } //need to force this to only be 5 long and numbers
        public DateTime BirthDate { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Liking")]
		public virtual ICollection<Liking> Likings { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Hobby")]
		public virtual ICollection<Hobby> HobbyList { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Photo")]
		public virtual Photo ProfilePhoto { get; set; }

		public Gender Gender { get; set; }
		public Pets? Pets { get; set; }
		public Religion? Religion { get; set; }
		public Job? Job { get; set; }
		public Education? Education { get; set; }
		public Ethnicity? Ethnicity { get; set; }
		public Athleticism? Athleticism { get; set; }
		public SexualOrientation? SexualOrientation { get; set; }
		public MariageStatus? MariageStatus { get; set; }
		public Children? Children { get; set; }
		public Smokes? Smokes { get; set; }
		public Drinks? Drinks { get; set; }
		public Drugs? Drugs { get; set; }
    }
}