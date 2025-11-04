import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { Transation } from '../../../models/transaction.model';

@Component({
  selector: 'app-delete-transation',
  imports: [],
  templateUrl: './delete-transation.component.html',
  styleUrl: './delete-transation.component.css',
})
export class DeleteTransationComponent {
  @ViewChild('modal') private modal?: ElementRef<HTMLDialogElement>;

  @Input() transationtoDelete?: Transation;

  @Output() deletedClick = new EventEmitter<void>();

  private get modalElement() {
    return this.modal?.nativeElement as HTMLDialogElement;
  }

  showModal() {
    this.modalElement.showModal();
  }

  closeModal() {
    this.modalElement.close();
  }

  deleteTransation() {
    this.deletedClick.emit();
    this.modalElement.close();
  }
}
