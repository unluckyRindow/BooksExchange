import { Component, OnInit, Inject } from '@angular/core';
import { Book, LiteraryGenre, BookCreateData } from '../../models/book.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BookDetailsComponent } from '../book-details/book-details.component';
import { FormBuilder, Validators } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { BooksService } from '../../services/books.service';
import { AuthService } from 'src/app/auth/services/auth.service';


export interface BookEditData {
  editMode: boolean;
  book?: Book;
}

@UntilDestroy()
@Component({
  selector: 'app-book-edit',
  templateUrl: './book-edit.component.html',
  styleUrls: ['./book-edit.component.scss']
})
export class BookEditComponent implements OnInit {

  private book: Book;
  public genres = Object.keys(LiteraryGenre);

  bookGroup = this.fb.group({
    title: [this.data.editMode ? this.data.book.title : '', Validators.required],
    author: [this.data.editMode ? this.data.book.author : '', Validators.required],
    releaseDate: [this.data.editMode ? this.data.book.releaseDate : ''],
    category: [this.data.editMode ? this.data.book.category : '', Validators.required],
    description: [this.data.editMode ? this.data.book.description : ''],
    visibility: [this.data.editMode ? this.data.book.status : '', Validators.required],
  });


  constructor(
    public dialogRef: MatDialogRef<BookDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: BookEditData,
    public fb: FormBuilder,
    private booksService: BooksService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    if (this.data.editMode) {
      this.book = this.data.book;
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    const updated: Book = {
      id: this.data.book.id,
      owner: this.data.book.owner,
      status: this.bookGroup.value.visibility,
      title: this.bookGroup.value.title,
      category: this.bookGroup.value.category,
      author: this.bookGroup.value.author,
      creationDate: this.data.book.creationDate,
      releaseDate: this.bookGroup.value.releaseDate,
      description: this.bookGroup.value.description,
    } as Book;
    this.booksService.updateBook(updated)
      .pipe(untilDestroyed(this))
      .subscribe(x => {
        this.dialogRef.close(true);
      });
  }

  onAdd(): void {
    const created: BookCreateData = {
      ownerId: this.authService.userId,
      status: this.bookGroup.value.visibility,
      title: this.bookGroup.value.title,
      category: this.bookGroup.value.category,
      author: this.bookGroup.value.author,
      releaseDate: this.bookGroup.value.releaseDate,
      description: this.bookGroup.value.description,
    } as BookCreateData;
    this.booksService.addBook(created)
      .pipe(untilDestroyed(this))
      .subscribe(x => {
        this.dialogRef.close(true);
      });
  }

}
