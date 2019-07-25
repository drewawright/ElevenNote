import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { MatToolbarModule, MatButtonModule, MatFormFieldModule, MatInputModule } from '@angular/material';
import { MatTableModule } from '@angular/material/table';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AuthService } from './services/auth.service';
import { LoginComponent } from './components/login/login.component';
import { NotesService } from './services/notes.service';
import { NoteIndexComponent } from './components/notes/note-index/note-index.component';
import { NoteCreateComponent } from './components/notes/note-create/note-create.component';
import { NoteDetailComponent } from './components/notes/note-detail/note-detail.component';
import { NoteEditComponent } from './components/notes/note-edit/note-edit.component';
import { NoteDeleteComponent } from './components/notes/note-delete/note-delete.component';
import { AuthGuard } from './guards/auth.guard';

const routes = [
  { path: 'register', component: RegistrationComponent},
  { path: 'login', component: LoginComponent},
  { 
    path: 'notes', canActivate: [AuthGuard] ,children: [
      {path: '', component: NoteIndexComponent },
      {path: 'create', component: NoteCreateComponent },
      {path: 'detail/:id', component: NoteDetailComponent}
    ]
  },
  { path: '**', component: RegistrationComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    RegistrationComponent,
    LoginComponent,
    NoteIndexComponent,
    NoteCreateComponent,
    NoteDetailComponent,
    NoteEditComponent,
    NoteDeleteComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule
  ],
  providers: [
    AuthService,
    NotesService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
