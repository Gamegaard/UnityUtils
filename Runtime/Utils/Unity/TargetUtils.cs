using UnityEngine;

namespace Gamegaard.Utils
{
    public static class TargetUtils
    {
        public static Transform GetRandom(Transform caster, Transform[] targets)
        {
            return targets.GetRandomExcept(caster);
        }

        public static Transform GetClosest(Transform caster, Transform[] targets)
        {
            if (targets.Length > 0)
            {
                Transform closet = null;
                float closestDistance = float.MaxValue;

                for (int i = 0; i < targets.Length; i++)
                {
                    Transform current = targets[i];
                    if (current.transform == caster || current.transform.IsChildOf(caster)) continue;
                    float actualDistance = Vector2Utils.GetDistance(caster.position, current.transform.position);

                    if (actualDistance < closestDistance)
                    {
                        closestDistance = actualDistance;
                        closet = current;
                    }
                }
                if (closet != null)
                    return closet?.transform;
            }

            return null;
        }

        public static Transform GetFarest(Transform caster, Transform[] targets)
        {
            if (targets.Length > 0)
            {
                Transform closet = null;
                float farestDistance = 0;

                for (int i = 0; i < targets.Length; i++)
                {
                    Transform current = targets[i];
                    if (current.transform == caster || current.transform.IsChildOf(caster)) continue;
                    float actualDistance = Vector2Utils.GetDistance(caster.position, current.transform.position);

                    if (actualDistance > farestDistance)
                    {
                        farestDistance = actualDistance;
                        closet = current;
                    }
                }
                if (closet != null)
                    return closet.transform;
            }

            return null;
        }
    }
}