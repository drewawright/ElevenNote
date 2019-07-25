import { Component, OnInit } from '@angular/core';
import { Note } from 'src/app/models/Note';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { NotesService } from 'src/app/services/notes.service';
import { ActivatedRoute, Router } from '@angular/router';
// import { format } from 'path';

@Component({
  selector: 'app-note-edit',
  templateUrl: './note-edit.component.html',
  styleUrls: ['./note-edit.component.css']
})
export class NoteEditComponent implements OnInit {

  note: Note;

  editNoteForm: FormGroup;

  constructor(private _form: FormBuilder,
              private _noteService: NotesService,
              private _ar: ActivatedRoute,
              private _router: Router) { 

                this._ar.paramMap.subscribe(p => {
                  this._noteService.getNote(p.get('id')).subscribe((singleNote: Note) => {
                    this.note = singleNote;
                    this.createForm();
                  });
                });
              }

  ngOnInit() {
  }

  createForm() {
    this.editNoteForm = this._form.group({
      NoteId: new FormControl(this.note.NoteId),
      IsStarred: new FormControl(this.note.IsStarred),
      Title: new FormControl(this.note.Title),
      Content: new FormControl(this.note.Content),
      CategoryID: new FormControl(this.note.CategoryID),
      CategoryName: new FormControl(this.note.CategoryName)
    });
  }

  onSubmit(form) {
    const updateNote: Note = {
      NoteId: form.value.NoteId,
      Title: form.value.Title,
      Content: form.value.Content,
      CategoryID: form.value.CategoryID,
      CategoryName: form.value.CategoryName,
      IsStarred: form.value.IsStarred
     };
    this._noteService.updateNote(updateNote).subscribe(d => {
      this._router.navigate(['/notes']);
    });
  }

}
