using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public class Save {
        public const int NB_SAVE = 3;
        public int SelectedSave { get; set; }
        public Characters[] CharactersArray { get; set; }

        public Save() {
            CharactersArray = new Characters[NB_SAVE];
            for (int i = 0; i < NB_SAVE; i++) {
                this.CharactersArray[i] = new Characters();
            }
            this.SelectedSave = 0;
        }
    }
}
