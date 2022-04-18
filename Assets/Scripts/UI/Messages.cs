using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class Messages {
        public List<string> msgs;
        public int idx;

        public Messages(List<string> msgs) {
            this.msgs = msgs;
            idx = -1;
        }

        public bool HasNext() {
            return idx < msgs.Count-1;
        }

        public string Next() {
            if (HasNext()) {
                return msgs[++idx];
            }

            return msgs[^1];
        }

        public void RevertLast() {
            idx = Mathf.Max(-1, idx - 1);
        }
    }
}