using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IRecordedEffect
{
    void Progress(float totalTime);
    void Destroy();
}

