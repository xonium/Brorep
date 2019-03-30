import { Component, OnInit, Input, SimpleChanges, SimpleChange } from '@angular/core';
import { RenderReactPlayer } from './reactplayer.standalone';
@Component({
  selector: 'app-video',
  template: `<div class="max-width-1024">
      <div id="player">
      </div>
    </div>`
})
export class VideoComponent implements OnInit {
  constructor() { }
  @Input() videoUrl: string;
  private _videoUrl: string;

  ngOnInit() {
        if (this._videoUrl) {
            const container = document.getElementById('player');
            const url = this.videoUrl;
            RenderReactPlayer(container, { url, playing: true });
        }
    }

    ngOnChanges(changes: SimpleChanges) {
        const inputvideoUrl: SimpleChange = changes.videoUrl;
        this._videoUrl = inputvideoUrl.currentValue;
        if (this._videoUrl) {
            const container = document.getElementById('player');
            const url = this.videoUrl;
            RenderReactPlayer(container, { url, playing: true, onPlay: this.onPlay, onProgress: this.onProgress });
        }
    }

    onPlay() {
        console.log('playing!');
    }

    onProgress(progress) {
        console.log('progress', progress);
    }
  }

