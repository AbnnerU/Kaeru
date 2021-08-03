using UnityEngine;

public interface IWater 
{
    float GetWaterLevel();
}

public interface ICanFlowPath
{
    int FlowPathId { get; set; }
}
