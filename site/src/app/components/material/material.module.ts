import { MatDialogRef, MatCheckboxModule, MAT_DIALOG_DATA, MatSnackBarModule, MatStepperModule, MatSelectModule, MatTableModule, MatSidenavModule, MatFormField, MatFormFieldModule, MatAccordion, MatExpansionModule, MatRadioModule } from '@angular/material';
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
    MatExpansionModule,
    MatSidenavModule,
    MatDialogModule,
    MatTableModule,
    MatInputModule,
    MatCardModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatSnackBarModule,
    MatStepperModule,
    MatSelectModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
  ],
  providers: [{ provide: MAT_DIALOG_DATA, useValue: {} },
  { provide: MatDialogRef, useValue: {} }]

})
export class MaterialModule { }
