using System;
using System.Collections.Generic;
using UnityEngine;

public class Carpenter : MapObject
{
    public List<HouseTier> houseTiers;
    [SerializeField]
    private CarpenterManager doorPopUp;

    public Home home;

    public void FixHouse(int fixIndex)
    {
        home.UpdateHouse(houseTiers[fixIndex].completion, houseTiers[fixIndex].home, houseTiers[fixIndex].inside);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        doorPopUp.Activate(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        doorPopUp.Hide();
    }

    [Serializable]
    public class HouseTier
    {
        public int value;
        public int completion;
        public Sprite home;
        public Texture inside;
    }
}
