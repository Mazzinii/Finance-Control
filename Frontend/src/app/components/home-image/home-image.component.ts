import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home-image',
  imports: [FormsModule],
  templateUrl: './home-image.component.html',
  styleUrl: './home-image.component.css',
})
export class HomeImageComponent {
  //@Input() componentFilho : string = '';
  @Input() componentFilho!: string;
  name = 'Home';
  userName = 'Eduardo';

  items = ['processador', 'placa mãe', 'placa de vídeo'];

  showMessage(): void {
    if (this.eventChanger != 'Clicou') {
      this.eventChanger = 'Clicou';
    } else {
      this.eventChanger = 'Clique em mim';
    }
  }

  eventChanger = 'Clique em mim';

  //implementar binding
  inputName: string = '';
  //aprender sobre outputs
  //aprender mais sobre rotas
  //Pegar dadaos de uma API
  //testar com a API
  //veriifcar composição de paginas para implementar o DRY
}
