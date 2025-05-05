using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Utils;

namespace AntiTower;

[MinimumApiVersion(132)]
public class AntiTower : BasePlugin
{
    public override string ModuleName => "AntiTower";

    public override string ModuleAuthor => "v4r1able";
    public override string ModuleDescription => "Prevents players from climbing the tower in awp_lego_2 map";
    public override string ModuleVersion => "1.0.0";

    private readonly Vector triggerPosition = new Vector(-43.303169f, 12.203090f, -189.015625f);
    private readonly float triggerRadius = 30f;

    private readonly Vector targetPosition = new Vector(-41.086075f, 44.352432f, -243.128754f);
    private readonly QAngle targetAngle = new QAngle(10.002346f, -89.681366f, 0.0f);

    public override void Load(bool hotReload)
    {
        RegisterListener<Listeners.OnTick>(() =>
        {
            foreach (var player in Utilities.GetPlayers())
            {
                if (player == null || !player.IsValid || player.PlayerPawn == null || !player.PawnIsAlive)
                    continue;

                var pos = player.PlayerPawn.Value.AbsOrigin;

                if (CalculateDistance(pos, triggerPosition) <= triggerRadius)
                {
                    player.PlayerPawn.Value.Teleport(targetPosition, targetAngle, new Vector(0, 0, 0));
                }
            }
        });
    }

    private float CalculateDistance(Vector v1, Vector v2)
    {
        var diffX = v1.X - v2.X;
        var diffY = v1.Y - v2.Y;
        var diffZ = v1.Z - v2.Z;
        return MathF.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ);
    }
}
