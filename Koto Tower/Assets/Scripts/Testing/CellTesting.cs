using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellContent
{
    TOWER,
    KOTO_TOWER,
    PATH,
    OPEN_FIELD,
    GENERATOR,
    DISABLED
}

public class CellTesting
{
    public CellContent cellContent = CellContent.OPEN_FIELD;

    public CellTesting()
    {
        cellContent = CellContent.OPEN_FIELD;
    }
}
