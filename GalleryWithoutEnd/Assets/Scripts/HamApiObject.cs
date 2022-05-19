using System;
using System.Collections.Generic;

namespace HamApi.ObjectInfo
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Color
    {
        public string color { get; set; }
        public string spectrum { get; set; }
        public string hue { get; set; }
        public double percent { get; set; }
        public string css3 { get; set; }
    }

    public class Image
    {
        public string date { get; set; }
        public string copyright { get; set; }
        public int imageid { get; set; }
        public int idsid { get; set; }
        public string format { get; set; }
        public object description { get; set; }
        public object technique { get; set; }
        public string renditionnumber { get; set; }
        public int displayorder { get; set; }
        public string baseimageurl { get; set; }
        public object alttext { get; set; }
        public int width { get; set; }
        public object publiccaption { get; set; }
        public string iiifbaseuri { get; set; }
        public int height { get; set; }
    }

    public class Info
    {
        public int totalrecordsperquery { get; set; }
        public int totalrecords { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public string next { get; set; }
        public string prev { get; set; }
    }

    public class Person
    {
        public string role { get; set; }
        public object birthplace { get; set; }
        public string gender { get; set; }
        public string displaydate { get; set; }
        public object prefix { get; set; }
        public string culture { get; set; }
        public string displayname { get; set; }
        public string alphasort { get; set; }
        public string name { get; set; }
        public int personid { get; set; }
        public object deathplace { get; set; }
        public int displayorder { get; set; }
    }

    public class Record
    {
        public object copyright { get; set; }
        public int contextualtextcount { get; set; }
        public string creditline { get; set; }
        public int accesslevel { get; set; }
        public string dateoflastpageview { get; set; }
        public int classificationid { get; set; }
        public string division { get; set; }
        public int markscount { get; set; }
        public int publicationcount { get; set; }
        public int totaluniquepageviews { get; set; }
        public string contact { get; set; }
        public int colorcount { get; set; }
        public int rank { get; set; }
        public object state { get; set; }
        public int id { get; set; }
        public string verificationleveldescription { get; set; }
        public object period { get; set; }
        public List<Image> images { get; set; }
        public List<Worktype> worktypes { get; set; }
        public int imagecount { get; set; }
        public int totalpageviews { get; set; }
        public int? accessionyear { get; set; }
        public object standardreferencenumber { get; set; }
        public object signed { get; set; }
        public string classification { get; set; }
        public int relatedcount { get; set; }
        public int verificationlevel { get; set; }
        public string primaryimageurl { get; set; }
        public int titlescount { get; set; }
        public int peoplecount { get; set; }
        public object style { get; set; }
        public DateTime lastupdate { get; set; }
        public object commentary { get; set; }
        public object periodid { get; set; }
        public object technique { get; set; }
        public object edition { get; set; }
        public object description { get; set; }
        public string medium { get; set; }
        public int lendingpermissionlevel { get; set; }
        public string title { get; set; }
        public string accessionmethod { get; set; }
        public List<Color> colors { get; set; }
        public object provenance { get; set; }
        public int groupcount { get; set; }
        public object dated { get; set; }
        public string department { get; set; }
        public int dateend { get; set; }
        public List<Person> people { get; set; }
        public string url { get; set; }
        public string dateoffirstpageview { get; set; }
        public object century { get; set; }
        public string objectnumber { get; set; }
        public object labeltext { get; set; }
        public int datebegin { get; set; }
        public object culture { get; set; }
        public int exhibitioncount { get; set; }
        public int imagepermissionlevel { get; set; }
        public int mediacount { get; set; }
        public int objectid { get; set; }
        public object techniqueid { get; set; }
        public object dimensions { get; set; }
        public List<SeeAlso> seeAlso { get; set; }
    }

    public class Root
    {
        public Info info { get; set; }
        public List<Record> records { get; set; }
    }

    public class SeeAlso
    {
        public string id { get; set; }
        public string type { get; set; }
        public string format { get; set; }
        public string profile { get; set; }
    }

    public class Worktype
    {
        public string worktypeid { get; set; }
        public string worktype { get; set; }
    }
}
