using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griffin : NPC
{  
    // Добавить название спела
    Brachiabindo brachiabindo = new Brachiabindo();

    /// <summary>
    /// Отвечает за связь с движениями уничтожения
    /// </summary>
    public void DestroyNPC()
    {
        if(brachiabindo.CompareSpellPattern() == true)
            print("Griffin has catched");
    }
}