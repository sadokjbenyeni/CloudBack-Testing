import { MatDialogRef, MatCheckboxModule, MAT_DIALOG_DATA, MatSnackBarModule } from '@angular/material';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Material Imports
import { MatDialogModule, MatInputModule, MatCardModule, MatProgressSpinnerModule, MatSnackBar } from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    MatDialogModule,
    MatInputModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatSnackBarModule,
  ],
  providers: [{provide: MAT_DIALOG_DATA, useValue: {}}, 
    {provide: MatDialogRef, useValue: {}}]

})
export class MaterialModule { }
