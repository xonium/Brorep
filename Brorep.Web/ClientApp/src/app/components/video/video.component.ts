import { Component, OnInit, Input, SimpleChanges, SimpleChange, EventEmitter, Output, OnChanges } from '@angular/core';
import { RenderReactPlayer } from './reactplayer.standalone';
@Component({
  selector: 'app-video',
  template: `<div class="max-width-1024">
      <div id="player">
      </div>
    </div>`
})
export class VideoComponent implements OnInit, OnChanges  {
  constructor() { }
  @Input() videoUrl: string;
  private _videoUrl: string;

  @Output() errorEvent = new EventEmitter();
  @Output() progressEvent = new EventEmitter();
  @Output() readyEvent = new EventEmitter();

  ngOnInit() {
        if (this._videoUrl) {
            const container = document.getElementById('player');
            const url = this.videoUrl;
            RenderReactPlayer(container, { url, playing: true });
        }
    }

    ngOnChanges(changes: SimpleChanges): void {
        const inputvideoUrl: SimpleChange = changes.videoUrl;
        const that = this;
        this._videoUrl = inputvideoUrl.currentValue;
        if (this._videoUrl) {
            const container = document.getElementById('player');
            const url = this.videoUrl;
            RenderReactPlayer(container,
                {
                    url,
                    playing: true,
                    onPlay: onVideoPlay,
                    onProgress: onVideoProgress,
                    onError: onVideoError,
                    onReady: onVideoReady
                });
        }

        function onVideoProgress(progress) {
            that.progressEvent.emit(progress);
        }

        function onVideoPlay() {
            console.log('playing!');
        }

        function onVideoError(error) {
            that.errorEvent.emit(error);
        }

        function onVideoReady() {
            that.readyEvent.emit();
        }
    }
  }

