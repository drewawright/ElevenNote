import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Note } from '../models/Note';

const ApiUrl = 'https://localhost:44327/api';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  constructor(private _http: HttpClient) { }

  getNotes() {
    return this._http.get(`${ApiUrl}/Notes`, { headers: this.getHeaders() });
  }

  getNote(id: string) {
    return this._http.get(`${ApiUrl}/Notes/${id}`, {headers: this.getHeaders() });
  }

  createNote(note: Note) {
    return this._http.post(`${ApiUrl}/Notes`, note, {headers: this.getHeaders() })
  }

  updateNote(note: Note){
    return this._http.put(`${ApiUrl}/Notes`, note, { headers: this.getHeaders() });
  }

  deleteNote(id: Number){
    return this._http.delete(`${ApiUrl}/Notes/${id}`, {headers: this.getHeaders() });
  }

  private getHeaders() {
    return new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('id_token')}`);
  }
}
