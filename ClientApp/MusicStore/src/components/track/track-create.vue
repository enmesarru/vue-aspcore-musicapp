<template>
  <div>
    <el-row id="test">
      <el-col :span="12">
        <el-form ref="trackForm" :model="track" label-width="120px" enctype="multipart/form-data">

          <el-form-item label="Track Name" prop="name">
            <el-input v-model="track.name"></el-input>
          </el-form-item>

          <el-form-item>
            <el-select v-model="track.albumId" placeholder="Select">
              <el-option
                v-for="item in albums"
                :key="item.id"
                :label="item.name"
                :value="item.id">
              </el-option>
            </el-select>
          </el-form-item>

          <el-form-item>
            <el-input-number v-model="track.number" :min="1" :max="255"></el-input-number>
          </el-form-item>

          <el-form-item>
            <input  ref="trackFile" @change="sendFileToData" type="file" :rules="[{ required: true, message: 'Album image is required'}]" />
          </el-form-item>

          <el-form-item>
            <el-button type="primary" @click="uploadFile()">Create</el-button>
            <el-button>Cancel</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
  </div>
</template>

<script>
    export default {
      name: 'track-create',
      created () {
        this.getAlbums()
      },
      methods: {
        sendFileToData () {
          this.track.trackFile = this.$refs.trackFile.files[0]
          console.log(this.track.trackFile)
        },
        uploadFile () {
          var form = new FormData()
          form.append('trackName', this.track.name)
          form.append('trackNumber', this.track.number)
          form.append('albumId', this.track.albumId)
          if (this.track.trackFile != null) {
            form.append('trackFile', this.track.trackFile)
          }
          this.$http.post('http://localhost:5000/api/tracks', form, {
            headers: {
              'Content-Type': 'multipart/form-data; application/json; charset=utf-8;'
            }
          })
        },
        getAlbums () {
          this.$http.get('http://localhost:5000/api/albums/')
            .then((response) => { this.albums = response.body }
              , (response) => { console.log(response.error) })
        }
      },
      data () {
        return {
          albums: [],
          track: {
            name: '',
            number: '',
            albumId: null,
            trackFile: []
          }
        }
      }
    }
</script>

<style scoped>

</style>
