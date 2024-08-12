import { Component, inject } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MatDialogTitle } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";

import { VotingType } from "../../voting-view/voting-view.component";

@Component({
    selector: 'add-modal',
    templateUrl: 'add-modal.html',
    standalone: true,
    imports: [
      MatFormFieldModule,
      MatInputModule,
      FormsModule,
      MatButtonModule,
      MatDialogTitle,
      MatDialogContent,
      MatDialogActions,
      MatDialogClose,
    ],
  })
  export class AddModal {
    readonly dialogRef = inject(MatDialogRef<AddModal>);
    readonly data = inject<VotingType>(MAT_DIALOG_DATA);
    
    fullName = "";
  
    add(): void {
        if (this.fullName.length >= 5 && this.fullName.length <= 50) {
            this.dialogRef.close({
                FullName: this.fullName,
                VotingType: this.data,
            });
        }
    }

    save(fullName: string): void {
        this.fullName = fullName;
    }
  }