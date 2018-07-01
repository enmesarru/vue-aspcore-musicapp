<template>
  <div id="app">
    <el-row>
      <el-col :span="24">
        <el-menu
          :default-active="activeIndex"
          class="el-menu-demo"
          mode="horizontal"
          @select="handleSelect"
          background-color="#545c64"
          text-color="#fff"
          active-text-color="#ffd04b">
          <el-menu-item index="1" >Genre</el-menu-item>
          <el-menu-item index="2">Artist</el-menu-item>
          <el-menu-item index="3">Album</el-menu-item>
          <el-menu-item index="4">Track</el-menu-item>
        </el-menu>
      </el-col>
    </el-row>
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'app',
  data () {
    return {
      activeIndex: '1'
    }
  },
  methods: {
    handleSelect (key, keyPath) {
      switch (key) {
        case '1': this.$router.push({name: 'genre-list'})
          break
        case '2': this.$router.push({name: 'artist-list'})
          break
        case '3': this.$router.push({name: 'album-list'})
          break
        case '4': this.$router.push({name: 'track-list'})
          break
      }
    }
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
