import { MatDialogRef, MatCheckboxModule, MAT_DIALOG_DATA, MatSnackBarModule, MatStepperModule, MatSelectModule, MatTableModule, MatSidenavModule, MatFormField, MatFormFieldModule, MatAccordion, MatExpansionModule, MatRadioModule, MatSortModule, MatSort, MatPaginator, MatPaginatorModule } from '@angular/material';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import 'hammerjs/hammer';

// Material Imports
import { MatDialogModule, MatInputModule, MatCardModule, MatProgressSpinnerModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    BrowserAnimationsModule,
    MatExpansionModule,
    MatSidenavModule,
    MatDialogModule,
    MatTableModule,
    MatInputModule,
    MatCardModule,
    MatSortModule,      
    MatPaginatorModule,
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
