import { MatDialogRef, MatCheckboxModule, MAT_DIALOG_DATA, MatSnackBarModule, MatStepperModule, MatSelectModule, MatTableModule, MatSidenavModule, MatFormField, MatFormFieldModule } from '@angular/material';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Material Imports
import { MatDialogModule, MatInputModule, MatCardModule, MatProgressSpinnerModule } from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    MatSidenavModule,
    MatDialogModule,
    MatTableModule,
    MatInputModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatSnackBarModule,
    MatStepperModule,
    MatSelectModule,
    MatFormFieldModule,
  ],
  providers: [{ provide: MAT_DIALOG_DATA, useValue: {} },
  { provide: MatDialogRef, useValue: {} }]

})
export class MaterialModule { }
