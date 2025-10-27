using System.Globalization;

public class LegoSet
{
    public LegoSet(string id, string name, int year, int pieces, int miniFigs, int? minAge, string imageUrl, double? retailPrice)
    {
        Id = id;
        Name = name;
        Year = year;
        Pieces = pieces;
        MiniFigs = miniFigs;
        MinAge = minAge;
        ImageUrl = imageUrl;
        RetailPrice = retailPrice;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    private int _pieces;
    public int Pieces {
        get
        {
            return _pieces;
        }

        set
        {
            if (value == null || value == 0)
            {
                throw new LegoException("no pieces");
            }
            else
            {
                _pieces = value;
            }
        }
    }
    private int _minifigs;
    public int MiniFigs { get
        {
            return _minifigs;
        } 
        set
        {
            if (value == null)
            {
                value = 0;
            }
            _minifigs = value;
        } 
    }
    private int? _minAge;
    public int? MinAge { get 
        {
            return _minAge;
        }
        set
        {
            if(value != null && value <= 0)
            {
                throw new LegoException("minimum leeftijd can not be 0 or lower");
            }
            else
            {
                _minAge = value;
            }
        }
    }
    public string ImageUrl { get; set; }
    private double? _retailPrice;
    public double? RetailPrice { 
        get 
        {
            return _retailPrice; 
        } 
        set
        {
            if (value != null && value <= 0)
            {
                throw new LegoException("price can not be 0 or lower");
            }
            else
            {
                _retailPrice = value;   
            }
        }
    }

    public override string ToString()
    {
        return $"{Id},{Name},{Year},{Pieces},{MiniFigs},{MinAge},{RetailPrice?.ToString(CultureInfo.InvariantCulture) ?? "null"},{ImageUrl}";
    }
}