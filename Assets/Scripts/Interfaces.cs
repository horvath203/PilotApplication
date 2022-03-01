using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------- BUILDINGS ------------

public interface BuildingType
{
    public bool CanBuild();
    public CountryManager.Resources ApplyConstructionCosts(CountryManager.Resources res);
    public RegionHandler.Region IncreaseProductions(RegionHandler.Region reg);
    public RegionHandler.Region RemoveProductions(RegionHandler.Region reg);
    public int RequiredWorkers();
    public string StringProductions();
    public bool TakesSlot();
}

public interface Acommodation : BuildingType
{
    public int Maintenance();

    public int ProvidedPopulation();
}

public interface Mines : BuildingType
{
    public int IronProduction();
}

public interface Lahka : BuildingType
{
    public int MoneyProduction();

    public int ElectronicProduction();

    public int ChemicalProduction();

    public int TextilProduction();
}

public interface Tazka : BuildingType
{
    public int TraktorProduction();

    public int TankProduction();

    public int IronExpense();
}

public interface Power : BuildingType
{
    public int PowerProduction();

    public int Maintenance();
}

public interface Special : BuildingType
{
    public int RequiredNextVar();
}



public interface Showcasable
{
    public GameObject GetLink();
}

// ----------- REGIONS -----------

public interface Clickable
{
    void OnMouseUpAsButton();
    public void SetSelected();
    public void SetUnselected();
}

public interface Hoverable
{
    void OnMouseEnter();
    void SetHovered();
    void OnMouseExit();
    void SetUnhovered();
}