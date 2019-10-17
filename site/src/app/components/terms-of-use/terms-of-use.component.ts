import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TermsService } from '../../services/terms.service';
import { Term } from '../../models/Terms';

@Component({
  selector: 'app-terms-of-use',
  templateUrl: './terms-of-use.component.html',
  styleUrls: ['./terms-of-use.component.css']
})
export class TermsOfUseComponent implements OnInit {

  acceptence: boolean;
  term: Term;
  constructor(private termService: TermsService, private dialogRef: MatDialogRef<TermsOfUseComponent>, @Inject(MAT_DIALOG_DATA) public data) { }

  ngOnInit() {
    this.acceptence = this.data["checked"]
    this.termService.getLastTerm().subscribe(result => {
      debugger;
      this.term = result;
    })
  }

  onclose() {
    debugger;
    this.dialogRef.close(this.acceptence);
  }
}
