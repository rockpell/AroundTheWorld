using System.Collections;
using System.Collections.Generic;

public class FishingRod
{
    protected int price;
    protected int fishingProbability;
    protected int durability;

    public int Price {
        get { return price; }
        set { price = value; }
    }

    public int FishingProbability {
        get { return fishingProbability; }
        set { fishingProbability = value; }
    }

    public int Durability {
        get { return durability; }
        set
        {
            durability = value;
            if (durability < 0) durability = 0;
        }
    }
}

public class FishingRodA : FishingRod
{
    public FishingRodA()
    {
        this.price = 10;
        this.fishingProbability = 10;
        this.durability = 100;
    }
}