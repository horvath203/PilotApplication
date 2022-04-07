using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --------- BUILDINGS ------------

public interface BuildingType
{
    public bool CanBuild();
    public void ApplyConstructionCosts(ref CountryManager.Resources res);
    public void ApplyProductions(ref CountryManager.Resources res);
    //public void IncreaseProductions(ref RegionHandler.Region reg);
    //public void RemoveProductions(ref RegionHandler.Region reg);
    public int RequiredWorkers();
    public string StringProductions();
    public bool TakesSlot(); //remove after we make slotstructure function (SS implicitely takes a slot, buildingtype without ss does not)
}

public interface SlotStructure : BuildingType
{
    //public void ApplyProductions(ref CountryManager.Resources res);
    //public int RequiredWorkers();
    //public string StringProductions();
}

public interface Acommodation : BuildingType
{
    public int ProvidedPopulation();
}

public interface Mines : BuildingType
{
    public int IronProduction();
}

public interface Lahka : BuildingType
{
    public int TextilProduction();
}

public interface Tazka : BuildingType
{
    public int TraktorProduction();

    public int IronExpense();
}

public interface Power : BuildingType
{
    public int EnergyProduction();
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
