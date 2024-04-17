using System.Collections.Generic;

public class MealCategory
{
    public List<SubCategory> SubCategories { get; set; }
    public string Title { get; set; }
    public string ItemSourceImage { get; set; }
}

public class SubCategory
{
    public string Title { get; set; }
    public ServingInfo EachServing { get; set; }
    public Dictionary<string, MealItem> Dishes { get; set; }
}

public class ServingInfo
{
    public string Gram { get; set; }
    public string Protein { get; set; }
    public string Fat { get; set; }
    public string KiloCal { get; set; }
}

public class MealItem
{
    public string Measure { get; set; }
    public string Amount { get; set; }
    public string ItemSourceImage { get; set; } = "";
}
