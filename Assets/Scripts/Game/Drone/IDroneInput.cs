using System;
using UnityEngine;

namespace YT.Game.Drone
{
    public interface IDroneInput
    {
        event Action<ViewMode> OnViewModeChanged;
        event Action OnBombDropped;
        Vector2 LookDirection { get; }
        Vector2 MoveDirection { get; }
        float SideRotation { get; }
        ViewMode CurrentViewMode { get; }
        float FlyUpSpeed { get; }
    }
}