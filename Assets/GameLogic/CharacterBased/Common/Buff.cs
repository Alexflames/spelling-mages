using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Buff{
    void Init();
    void Update();
    bool IsActive();
    void Destroy();
}
