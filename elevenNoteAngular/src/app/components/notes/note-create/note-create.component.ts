import { Component, OnInit } from '@angular/core';
import { NotesService } from '../../../services/notes.service';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-note-create',
  templateUrl: './note-create.component.html',
  styleUrls: ['./note-create.component.css']
})
export class NoteCreateComponent implements OnInit {

  noteForm: FormGroup;

  constructor(private _noteService: NotesService, private _form: FormBuilder, private _router: Router) { 
    this.createForm();
  }

  ngOnInit() {
  }

  createForm() {
    this.noteForm = this._form.group({
      Title: new FormControl,
      Content: new FormControl,
      CategoryID: new FormControl,
      CategoryName: new FormControl
    });
  }

  onSubmit() {
    this._noteService.createNote(this.noteForm.value).subscribe(data => {
      this._router.navigate(['/notes']);
    })
  }

}
