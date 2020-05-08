using System;
using System.Collections.Generic;

namespace api_widepay.Utils {
    public static class Plano {
        public static List<string> retornaVelocidade (int velocidade) {

            return new List<string> {
                String.Format ("=max-limit={0}M/{0}M", velocidade),
                String.Format ("=burst-limit=0/{1}M", velocidade, velocidade * 2),
                String.Format ("=burst-threshold=0M/{0}K", (velocidade * 0.7) * 1000),
                String.Format ("=burst-time=0/35")

            };

        }
    }
}